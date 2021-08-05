using System;
using Adapter;
using Data;
using Domain.Entities;
using Domain.Interfaces.Adapters;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Interfaces.Visitors;
using Domain.Results;
using Service;
using Unity;
using Unity.Lifetime;
using Unity.log4net;
using Visitor;

namespace CrossCutting
{
    public static class DIRegister
    {
        public static void Register(IUnityContainer container)
        {
            //Services...
            container.RegisterType<ICourseService, CourseService>(new PerResolveLifetimeManager());
            container.RegisterType<IStudentService, StudentService>(new PerResolveLifetimeManager());

            //Repositories...
            container.RegisterType<ICourseRepository, CourseRepository>(new PerResolveLifetimeManager());
            container.RegisterType<IStudentRepository, StudentRepository>(new PerResolveLifetimeManager());

            //Visitors...
            container.RegisterType<IMessageQueueVisitor<Student>, MessageStudentCourseVisitor>(new PerResolveLifetimeManager());
            container.RegisterType<IMailVisitor<Student>, StudentMailVisitor>(new PerResolveLifetimeManager());

            //Adapters...
            container.RegisterType<IAdapter<CourseGeneralReport>, CourseReportAdapter>(new PerResolveLifetimeManager());

            //Extensions...
            container.AddNewExtension<Log4NetExtension>();
        }
    }
}
