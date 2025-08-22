using Microsoft.JSInterop;
using System.Threading.Tasks;


public class JSInteropHelper
{
    private readonly IJSRuntime _jsRuntime;

    public JSInteropHelper(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public ValueTask<T> InvokeAsync<T>(string identifier, params object[] args)
    {
        return _jsRuntime.InvokeAsync<T>(identifier, args);
    }

    public ValueTask InvokeVoidAsync(string identifier, params object[] args)
    {
        return _jsRuntime.InvokeVoidAsync(identifier, args);
    }
    // Method to call a JavaScript function
    public ValueTask InvokeLayoutInit()
    {
        return _jsRuntime.InvokeVoidAsync("Layout.init");
    }
}