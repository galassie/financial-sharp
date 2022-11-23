FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /lib/financial-sharp
COPY . /lib/financial-sharp
RUN dotnet build