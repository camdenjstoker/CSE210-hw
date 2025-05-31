public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        foreach (string word in text.Split(' '))
        {
            _words.Add(new Word(word));
        }
    }

    public void HideRandomWords(int numberToHide)
    {
        Random rand = new Random();
        int hidden = 0;

        while (hidden < numberToHide)
        {
            int i = rand.Next(_words.Count);

            if (!_words[i].IsHidden())
            {
                _words[i].Hide();
                hidden++;
            }

            // Optional: break if all visible words are already hidden
            if (_words.All(w => w.IsHidden()))
            {
                break;
            }
        }
    }

    public string GetDisplayText()
    {
        string wordsText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()} {wordsText}";
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden());
    } 
}

