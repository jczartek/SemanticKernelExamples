To run examples that use an `apiKey` in C#, you can use the `User Secrets` mechanism to securely store sensitive information in your local development environment. Here are the steps on how to do this:

1. **Add User Secrets to Your Project**

   Ensure your C# project has a `.csproj` file. To add User Secrets, open a terminal in your project directory and execute the following command:

   ```bash
   dotnet user-secrets init
   ```

   This command will add a section to your `.csproj` file that looks like this:

   ```xml
   <PropertyGroup>
     <UserSecretsId>your-guid-here</UserSecretsId>
   </PropertyGroup>
   ```

2. **Set the `apiKey` as a User Secret**

   To add the `apiKey`, use the following command in the terminal:

   ```bash
   dotnet user-secrets set "ApiSettings:ApiKey" "your-api-key-here"
   ```

   Here, `ApiSettings:ApiKey` means the key will be placed under the `ApiSettings` section, with the name `ApiKey`.

3. **Access the `apiKey` in C# Code**

   In your C# code, you can now access your `apiKey` using `IConfiguration`. To do this, make sure you have configured the system to load secrets. For example:

   ```csharp
   using Microsoft.Extensions.Configuration;

   public class Program
   {
       public static void Main(string[] args)
       {
           var builder = new ConfigurationBuilder()
               .AddUserSecrets<Program>();

           IConfiguration configuration = builder.Build();

           string apiKey = configuration["ApiSettings:ApiKey"];

           Console.WriteLine($"Your API Key is: {apiKey}");
       }
   }
   ```

4. **Notes**

   - `AddUserSecrets<Program>()` allows loading the secrets into your configuration.
   - `IConfiguration` is the standard way of accessing application settings, including keys stored as User Secrets.

Remember that `User Secrets` are intended only for use in the development environment and should not be used in production. For production, it is better to store keys in a secure secrets manager, such as Azure Key Vault.
