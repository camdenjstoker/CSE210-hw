
public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by guiding you through slow breathing.")
    {
    }
    public void RunActivity()
    {
        StartActivity();
        int interval = 0; // 3 seconds in, 3 seconds out

        while (interval < GetDuration())
        {
            Console.Write("\nBreathe in...\n");
            DisplayCountdown(4);

            Console.Write("\nNow breathe out...\n");
            DisplayCountdown(6);

            interval += 10; // 4 in + 6 out
        }

        EndActivity(); // show closing mesage
    }
}