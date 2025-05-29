using System.Reflection.PortableExecutable;

public class word
{

    private string _text;

    // set is hidden variable type
    private bool _isHidden;


    public word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public string GetDisplayText()
    {
        if (_isHidden)
        {
            return "___";
        }
        else
        {
            return _text;
        }
    }


}