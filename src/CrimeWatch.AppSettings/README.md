# Setting Environment Variables

CrimeWatch/src/CrimeWatch.AppSettings >

1. Set SQL Connection String.
    ```shell
    dotnet user-secrets set "SqlServerOptions:ConnectionString" CONNECTIONSTRINGS_DEFAULTCONNECTION
    ```

2. Set Azure Blob Storage Connection String.

    ```shell
    dotnet user-secrets set "BlobStorageOptions:ConnectionString" CONNECTIONSTRINGS_STORAGECONNECTION
    ```
   [Learn more about Azure Blob Storage](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction)\
   [Learn about Azurite Emulator for Development & Testing](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite)

3. Set Options for JSON Web Token.
    ```shell
    dotnet user-secrets set "JwtOptions:Secret" JWT_SECRET
    dotnet user-secrets set "JwtOptions:Issuer" JWT_ISSUER
    dotnet user-secrets set "JwtOptions:Audience" JWT_AUDIENCE
    ```