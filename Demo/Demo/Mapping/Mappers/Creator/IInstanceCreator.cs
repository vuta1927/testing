using System.Reflection.Emit;
using Demo.Mapping.Runtime;

namespace Demo.Mapping.Mappers.Creator
{
    internal interface IInstanceCreator<TTarget>
    {
        void Compile(ModuleBuilder builder);

        void Emit(CompilationContext context);
    }
}
