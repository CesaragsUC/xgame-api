#aguardando 90 segundos para aguardar o provisionamento e start do banco
sleep 5s
#rodar o comando para criar o banco
/opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U SA -P "b39ff7d2-3bc2-4e9f-bb3b-0c371ce81aad" -i simple-api-db.sql