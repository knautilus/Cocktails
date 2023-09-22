using Cocktails.Cqrs.Mediator.Commands;
using Cocktails.Cqrs.Mediator.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cocktails.Cqrs.Mediator
{
    public static class RegistrationExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection serviceCollection, Type mediatorImplementationType)
        {
            serviceCollection.AddTransient(typeof(IQueryProcessor), typeof(QueryProcessor));
            serviceCollection.AddTransient(typeof(ICommandProcessor), typeof(CommandProcessor));
            serviceCollection.AddTransient<ServiceFactory>(p => p.GetService);

            ConnectImplementationsToTypesClosing(typeof(IQueryHandler<,>), serviceCollection, mediatorImplementationType.Assembly);
            ConnectImplementationsToTypesClosing(typeof(ICommandHandler<,>), serviceCollection, mediatorImplementationType.Assembly);

            return serviceCollection;
        }

        private static void ConnectImplementationsToTypesClosing(Type openRequestInterface, IServiceCollection serviceCollection, Assembly mediatorAssembly)
        {
            var concretions = new List<Type>();
            var interfaces = new List<Type>();

            foreach (var type in mediatorAssembly.DefinedTypes.Where(t => !t.IsOpenGeneric()))
            {
                var interfaceTypes = type.FindInterfacesThatClose(openRequestInterface).ToArray();
                if (!interfaceTypes.Any()) continue;

                if (type.IsConcrete())
                {
                    concretions.Add(type);
                }

                foreach (var interfaceType in interfaceTypes)
                {
                    interfaces.Fill(interfaceType);
                }
            }

            foreach (var @interface in interfaces)
            {
                var exactMatches = concretions.Where(x => x.CanBeCastTo(@interface)).ToList();

                if (exactMatches.Count > 1)
                {
                    exactMatches.RemoveAll(m => !IsMatchingWithInterface(m, @interface));
                }

                foreach (var type in exactMatches)
                {
                    serviceCollection.TryAddTransient(@interface, type);
                }

                if (!@interface.IsOpenGeneric())
                {
                    AddConcretionsThatCouldBeClosed(@interface, concretions, serviceCollection);
                }
            }
        }

        private static bool IsOpenGeneric(this Type type)
        {
            return type.IsGenericTypeDefinition || type.ContainsGenericParameters;
        }

        private static bool IsConcrete(this Type type)
        {
            return !type.IsAbstract && !type.IsInterface;
        }

        private static bool CanBeCastTo(this Type pluggedType, Type pluginType)
        {
            if (pluggedType == null) return false;

            if (pluggedType == pluginType) return true;

            return pluginType.IsAssignableFrom(pluggedType);
        }

        private static bool IsMatchingWithInterface(Type? handlerType, Type handlerInterface)
        {
            if (handlerType == null || handlerInterface == null)
            {
                return false;
            }

            if (handlerType.IsInterface)
            {
                if (handlerType.GenericTypeArguments.SequenceEqual(handlerInterface.GenericTypeArguments))
                {
                    return true;
                }
            }
            else
            {
                return IsMatchingWithInterface(handlerType.GetInterface(handlerInterface.Name), handlerInterface);
            }

            return false;
        }

        private static void AddConcretionsThatCouldBeClosed(Type @interface, List<Type> concretions, IServiceCollection services)
        {
            foreach (var type in concretions.Where(x => x.IsOpenGeneric() && x.CouldCloseTo(@interface)))
            {
                try
                {
                    services.TryAddTransient(@interface, type.MakeGenericType(@interface.GenericTypeArguments));
                }
                catch (Exception)
                {
                }
            }
        }

        private static void Fill<T>(this IList<T> list, T value)
        {
            if (list.Contains(value)) return;
            list.Add(value);
        }

        internal static IEnumerable<Type> FindInterfacesThatClose(this Type pluggedType, Type templateType)
        {
            return FindInterfacesThatClosesCore(pluggedType, templateType).Distinct();
        }

        internal static bool CouldCloseTo(this Type openConcretion, Type closedInterface)
        {
            var openInterface = closedInterface.GetGenericTypeDefinition();
            var arguments = closedInterface.GenericTypeArguments;

            var concreteArguments = openConcretion.GenericTypeArguments;
            return arguments.Length == concreteArguments.Length && openConcretion.CanBeCastTo(openInterface);
        }

        private static IEnumerable<Type> FindInterfacesThatClosesCore(Type pluggedType, Type templateType)
        {
            if (pluggedType == null) yield break;

            if (!pluggedType.IsConcrete()) yield break;

            if (templateType.IsInterface)
            {
                foreach (
                    var interfaceType in
                    pluggedType.GetInterfaces()
                        .Where(type => type.IsGenericType && (type.GetGenericTypeDefinition() == templateType)))
                {
                    yield return interfaceType;
                }
            }
            else if (pluggedType.BaseType!.IsGenericType &&
                     (pluggedType.BaseType!.GetGenericTypeDefinition() == templateType))
            {
                yield return pluggedType.BaseType!;
            }

            if (pluggedType.BaseType == typeof(object)) yield break;

            foreach (var interfaceType in FindInterfacesThatClosesCore(pluggedType.BaseType!, templateType))
            {
                yield return interfaceType;
            }
        }
    }
}
