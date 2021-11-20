using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Services
{
    /// <summary>
    /// <see cref="http://www.ozcandegirmenci.com/create-object-instances-faster-than-reflection/"/>
    /// </summary>
    public class ObjectService : IObjectService
    {
        delegate object MethodInvoker();

        private MethodInvoker methodHandler = null;

        public ObjectService(Type type)
        {
            CreateMethod(type.GetConstructor(Type.EmptyTypes));
        }

        public ObjectService(ConstructorInfo target)
        {
            CreateMethod(target);
        }

        void CreateMethod(ConstructorInfo target)
        {
            DynamicMethod dynamic = new DynamicMethod(string.Empty,
                typeof(object),
                new Type[0],
                target.DeclaringType);
            ILGenerator il = dynamic.GetILGenerator();
            il.DeclareLocal(target.DeclaringType);
            il.Emit(OpCodes.Newobj, target);
            il.Emit(OpCodes.Stloc_0);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);

            methodHandler = (MethodInvoker) dynamic.CreateDelegate(typeof(MethodInvoker));
        }

        public object CreateInstance()
        {
            return methodHandler();
        }
    }

    internal interface IObjectService
    {
        object CreateInstance();
    }
}
