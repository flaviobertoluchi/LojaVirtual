FROM mcr.microsoft.com/mssql/server
EXPOSE 1433
COPY ./Tools/SqlServer/setup.sql .
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=Loja(!)Virtual
RUN /opt/mssql/bin/sqlservr & sleep 30 && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'Loja(!)Virtual' -i setup.sql