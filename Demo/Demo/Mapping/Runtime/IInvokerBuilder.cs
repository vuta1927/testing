using System.Reflection.Emit;

namespace Demo.Mapping.Runtime
{
    internal interface IInvokerBuilder
    {
        void Compile(ModuleBuilder builder);

        void Emit(CompilationContext context);
    }
}
