using Demo.Mapping.Runtime;

namespace Demo.Mapping.Conventions
{
    internal interface IMemberBuilder
    {
        void EmitGetter(CompilationContext context);

        void EmitSetter(CompilationContext context);
    }
}
