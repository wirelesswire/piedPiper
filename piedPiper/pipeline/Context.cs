public partial class PipelineSystem
{
    // --- 1. Context Class (Nested) ---
    public class Context
    {
        public Context()
        {
            UniqueToken = Guid.NewGuid();
            Logs = new List<string>();

        }

        public Guid UniqueToken { get; }
        public DateTime ProcessStartedAt { get; set; }
        public DateTime ProcessEndedAt { get; set; }

        public long ProcessTimeInMilliseconds
        {
            get
            {
                // Handle cases where End might not be set yet if accessed prematurely
                if (ProcessEndedAt < ProcessStartedAt) return -1;
                return (long)ProcessEndedAt.Subtract(ProcessStartedAt).TotalMilliseconds;
            }
        }

        public List<string> Logs { get; set; }

        public void Log(string message)
        {
            Logs.Add($"{DateTime.Now:O} [{UniqueToken}] - {message}");
        }
    }
}