using Autofac;
using OOP_Datatable_Dependency_Injection;

var container = ContainerConfig.Configure();
using(var scope = container.BeginLifetimeScope())
{
    //code starts in application.cs run method
    var app = scope.Resolve<IApplication>();
    app.Run();
}

