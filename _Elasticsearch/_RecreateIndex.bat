set url="http://1vm0001.dev.eda.rambler.tech:9201"

call :RecreateIndex "cocktails", "Cocktails.json"

exit /B 0

:RecreateIndex
curl -XDELETE "%url%/%~1"
curl -XPUT "%url%/%~1" -d "@%~2" -H "Content-Type: application/json"
curl "%url%/%~1?pretty"
exit /B 0