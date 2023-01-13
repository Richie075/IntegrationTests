FROM mcr.microsoft.com/mssql/server:2022-latest
                
        ENV ACCEPT_EULA=Y
        ENV SA_PASSWORD=PaSSw0rd_04
        ENV MSSQL_PID=Developer
        ENV MSSQL_TCP_PORT=1433 
        
        #ENV DEBIAN_FRONTEND noninteractive
        
        #COPY PU1simulator /app/Simulator 
        
        RUN (/opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" &&  /opt/mssql-tools/bin/sqlcmd -S127.0.0.1 -Usa -PPaSSw0rd_04
        

USER mssql

CMD bash