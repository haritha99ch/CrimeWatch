# Setting Environment Variables

CrimeWatch/src/CrimeWatch.AppSettings >

1. Set SQL Connection String.

    ```shell
    dotnet user-secrets set "ConnectionStrings:Database:DefaultConnection" CONNECTIONSTRINGS_DEFAULTCONNECTION
    ```
2. Set Azure Blob Storage Connection String.

    ```shell
    dotnet user-secrets set "ConnectionStrings:Storage:DefaultConnection" CONNECTIONSTRINGS_STORAGECONNECTION
    ```

    [Learn more about Azure Blob Storage](https://learn.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction)\
    [Learn about Azurite Emulator for Development & Testing](https://learn.microsoft.com/en-us/azure/storage/common/storage-use-azurite)
