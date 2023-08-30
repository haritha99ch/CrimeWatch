# Crime Watch

Crime Watch is an innovative web application that offers a unique solution to the problem of crime. It allows citizens to report any criminal activities or illegal incidents they witness directly to the proper authorities for investigation and judgment. This provides an effective way for law enforcement officials to investigate and judge the outcome of these reported crimes, as well as providing peace of mind for those who have witnessed them.

The Crime Watch system works by allowing users to upload photos, videos, or descriptions related to their incident in order provide evidence and details about what happened. The information is then sent immediately into a secure database maintained by the police department and notify the online police officers ensuring it gets seen right away so appropriate action can be taken swiftly if necessary.

This is a web application that uses .NET and React. It is a full-stack solution that uses MSSQl, ASP.NET Core and React.

## Setup

### Prerequisite

1. .NET 8 installed on your computer. You can download .NET 8 from the official website (<https://dotnet.microsoft.com/download/dotnet/8.0>).
2. Addition to that you must install Node 18 (<https://nodejs.org/en/download>).
3. An integrated development environment (IDE) to write your code
4. Git installed on your computer.

### Initial setup

1. Clone the project.

    ```shell
    git clone https://github.com/haritha99ch/CrimeWatch.git
    ```

2. [Set Environment variables](./src/CrimeWatch.AppSettings/README.md#setting-environment-variables).

3. Install all the dependencies.

    ```shell
    dotnet restore
    ```

    Install all the dependencies for [React project](./src/crimewatch.web.client/README.md#initial-setup)

4. Build the project. This will build the ASP.NET Core API alongside with the React frontend.

    ```shell
    dotnet build
    ```

5. Run the project.

    ```shell
    dotnet run --project ./src/CrimeWatch.Web.API/
    ```

6. Navigate to `http://localhost:5167/` for the development application.
