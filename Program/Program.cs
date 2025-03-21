using EfCoreExample;
using EfCoreExample.Models;
using Microsoft.EntityFrameworkCore;


/*
Scaffold-DbContext 'Data Source=SILVERSTONE\SQLEXPRESS;Initial Catalog=Students2;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Command Timeout=0' Microsoft.EntityFrameworkCore.SqlServer -Force
*/

Console.WriteLine("Hello World!");
using (var context = new Students2Context())
{
    new App(context).Run();
}


