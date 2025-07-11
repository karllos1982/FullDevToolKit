using FullDevToolKit.Common;

namespace FullDevToolKit.Core
{
    [Obsolete]
    public interface IContextBuilder
    {
        ISettings Settings { get; set; }

        void BuilderContext(IContext context);


    }
}
