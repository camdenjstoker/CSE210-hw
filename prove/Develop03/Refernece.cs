using System;
using System.Collections.Concurrent;

public class reference
{

    // The book it is from
    private string _book;

    // chapter number
    private int _chapter;


    // Verse Number
    private int _verse;

    // The starting verse
    private int _verseStart;

    // The Ending Verse
    private int _endVerse;

    // Constructor If the verse has only one verse
    public reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
    }

    // Constructor Only if the verse has many verses
    public reference(string book, int chapter, int verseStart, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verseStart = verseStart;
        _endVerse = endVerse;
    }

    // Display reference as a string
    public string GetDisplayText()
    {
        if (_verseStart == _endVerse)
        {
            return $"{_book} {_chapter}:{_verseStart}";
        }
        else
        {
            return $"{_book} {_chapter}:{_verseStart}-{_endVerse}";
        }
    }
}