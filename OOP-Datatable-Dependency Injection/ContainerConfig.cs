using Autofac;

namespace OOP_Datatable_Dependency_Injection
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<DataTableControl>().As<IDataTableControl>();
            builder.RegisterType<ExcelControl>().As<IExcelControl>();
            builder.RegisterType<SqlServerConnection>().As<ISqlServerConnection>();

            return builder.Build();
        }
    }
}
