using System;

namespace Cocktails.ViewModels
{
    public class IdModel
    {
        private readonly Guid _id;

        public IdModel(Guid id)
        {
            _id = id;
        }

        public Guid Id => _id;
    }
}
