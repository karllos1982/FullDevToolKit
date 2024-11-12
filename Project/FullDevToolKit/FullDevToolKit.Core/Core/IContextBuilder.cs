using FullDevToolKit.Common;

namespace FullDevToolKit.Core
{
    public interface IContextBuilder
    {
        ISettings Settings { get; set; }

        void BuilderContext(IContext context);


    }
}
