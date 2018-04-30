using AngleSharp.Dom;

namespace ScheduleServer.Converters {
    public interface IModelHtmlConverter<V> : IConvertable<IElement, V> { }
}