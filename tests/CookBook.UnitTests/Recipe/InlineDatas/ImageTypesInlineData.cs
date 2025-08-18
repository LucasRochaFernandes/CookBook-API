using CommonTestUtilities.Requests;
using System.Collections;

namespace CookBook.UnitTests.Recipe.InlineDatas;
public class ImageTypesInlineData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var images = FormFileBuilder.ImageCollection();
        foreach (var image in images)
            yield return new object[] { image };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
