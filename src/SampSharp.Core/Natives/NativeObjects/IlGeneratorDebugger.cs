using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace SampSharp.Core.Natives.NativeObjects
{
    internal class IlGeneratorDebugger
    {
        private readonly ILGenerator _inner;
        private readonly StringBuilder _sb = new();

        public IlGeneratorDebugger(ILGenerator inner)
        {
            _inner = inner;
        }

        public override string ToString()
        {
            return _sb.ToString();
        }

        public void Emit(OpCode op)
        {
            _sb.AppendLine(op.ToString());
            _inner.Emit(op);
        }

        public void Emit(OpCode op, int a)
        {
            _sb.AppendLine($"{op} {a}");
            _inner.Emit(op, a);
        }

        public void Emit(OpCode op, LocalBuilder a)
        {
            _sb.AppendLine($"{op} {a}");
            _inner.Emit(op, a);
        }

        public void Emit(OpCode op, FieldInfo a)
        {
            _sb.AppendLine($"{op} {a}");
            _inner.Emit(op, a);
        }

        public void Emit(OpCode op, ConstructorInfo a)
        {
            _sb.AppendLine($"{op} {a}");
            _inner.Emit(op, a);
        }

        public void Emit(OpCode op, Label a)
        {
            _sb.AppendLine($"{op} {a}");
            _inner.Emit(op, a);
        }

        public void Emit(OpCode op, string a)
        {
            _sb.AppendLine($"{op} {a}");
            _inner.Emit(op, a);
        }

        public void EmitCall(OpCode op, MethodInfo m, Type[] a)
        {
            _sb.AppendLine($"{op} {m}");
            _inner.EmitCall(op, m, a);
        }

        public LocalBuilder DeclareLocal(Type t)
        {
            _sb.AppendLine($"local {t}");
            return _inner.DeclareLocal(t);
        }

        public Label DefineLabel()
        {
            return _inner.DefineLabel();
        }

        public void MarkLabel(Label l)
        {
            _sb.AppendLine($"label {l}:");
            _inner.MarkLabel(l);
        }
    }
}