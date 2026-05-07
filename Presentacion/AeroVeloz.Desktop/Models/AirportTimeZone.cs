namespace AeroVeloz.Desktop.Models;


public class AirportTimeZone
{
    public string DisplayName { get; set; } = string.Empty;
    public string Offset { get; set; } = string.Empty;
    public TimeSpan TimeSpan { get; set; }

    public override string ToString() => DisplayName;

    public static IReadOnlyList<AirportTimeZone> GetValidTimeZones()
    {
        return new List<AirportTimeZone>
        {
            new() { DisplayName = "(UTC-12:00) Internacional Date Line West", Offset = "-12:00", TimeSpan = TimeSpan.FromHours(-12) },
            new() { DisplayName = "(UTC-11:00) Solomon Islands, Midway Island", Offset = "-11:00", TimeSpan = TimeSpan.FromHours(-11) },
            new() { DisplayName = "(UTC-10:00) Hawaii", Offset = "-10:00", TimeSpan = TimeSpan.FromHours(-10) },
            new() { DisplayName = "(UTC-09:30) Marquesas Islands", Offset = "-09:30", TimeSpan = TimeSpan.FromHours(-9).Add(TimeSpan.FromMinutes(-30)) },
            new() { DisplayName = "(UTC-09:00) Alaska, Gambier Islands", Offset = "-09:00", TimeSpan = TimeSpan.FromHours(-9) },
            new() { DisplayName = "(UTC-08:30) Chatham Islands", Offset = "-08:30", TimeSpan = TimeSpan.FromHours(-8).Add(TimeSpan.FromMinutes(-30)) },
            new() { DisplayName = "(UTC-08:00) Pacific Time (US & Canada)", Offset = "-08:00", TimeSpan = TimeSpan.FromHours(-8) },
            new() { DisplayName = "(UTC-07:00) Mountain Time (US & Canada)", Offset = "-07:00", TimeSpan = TimeSpan.FromHours(-7) },
            new() { DisplayName = "(UTC-06:00) Central Time (US & Canada)", Offset = "-06:00", TimeSpan = TimeSpan.FromHours(-6) },
            new() { DisplayName = "(UTC-05:00) Eastern Time (US & Canada)", Offset = "-05:00", TimeSpan = TimeSpan.FromHours(-5) },
            new() { DisplayName = "(UTC-04:30) Caracas", Offset = "-04:30", TimeSpan = TimeSpan.FromHours(-4).Add(TimeSpan.FromMinutes(-30)) },
            new() { DisplayName = "(UTC-04:00) Atlantic Time", Offset = "-04:00", TimeSpan = TimeSpan.FromHours(-4) },
            new() { DisplayName = "(UTC-03:30) Newfoundland", Offset = "-03:30", TimeSpan = TimeSpan.FromHours(-3).Add(TimeSpan.FromMinutes(-30)) },
            new() { DisplayName = "(UTC-03:00) Brasília, Buenos Aires", Offset = "-03:00", TimeSpan = TimeSpan.FromHours(-3) },
            new() { DisplayName = "(UTC-02:00) Mid-Atlantic", Offset = "-02:00", TimeSpan = TimeSpan.FromHours(-2) },
            new() { DisplayName = "(UTC-01:00) Azores, Cape Verde", Offset = "-01:00", TimeSpan = TimeSpan.FromHours(-1) },
            new() { DisplayName = "(UTC+00:00) GMT, London, Dublin", Offset = "+00:00", TimeSpan = TimeSpan.Zero },
            new() { DisplayName = "(UTC+01:00) CET, Paris, Berlin", Offset = "+01:00", TimeSpan = TimeSpan.FromHours(1) },
            new() { DisplayName = "(UTC+02:00) Cairo, Athens, Istanbul", Offset = "+02:00", TimeSpan = TimeSpan.FromHours(2) },
            new() { DisplayName = "(UTC+03:00) Moscow, Baghdad", Offset = "+03:00", TimeSpan = TimeSpan.FromHours(3) },
            new() { DisplayName = "(UTC+03:30) Tehran", Offset = "+03:30", TimeSpan = TimeSpan.FromHours(3).Add(TimeSpan.FromMinutes(30)) },
            new() { DisplayName = "(UTC+04:00) Dubai, Baku", Offset = "+04:00", TimeSpan = TimeSpan.FromHours(4) },
            new() { DisplayName = "(UTC+04:30) Kabul", Offset = "+04:30", TimeSpan = TimeSpan.FromHours(4).Add(TimeSpan.FromMinutes(30)) },
            new() { DisplayName = "(UTC+05:00) Pakistan", Offset = "+05:00", TimeSpan = TimeSpan.FromHours(5) },
            new() { DisplayName = "(UTC+05:30) India, Sri Lanka", Offset = "+05:30", TimeSpan = TimeSpan.FromHours(5).Add(TimeSpan.FromMinutes(30)) },
            new() { DisplayName = "(UTC+05:45) Nepal", Offset = "+05:45", TimeSpan = TimeSpan.FromHours(5).Add(TimeSpan.FromMinutes(45)) },
            new() { DisplayName = "(UTC+06:00) Bangladesh, Urumqi", Offset = "+06:00", TimeSpan = TimeSpan.FromHours(6) },
            new() { DisplayName = "(UTC+06:30) Myanmar", Offset = "+06:30", TimeSpan = TimeSpan.FromHours(6).Add(TimeSpan.FromMinutes(30)) },
            new() { DisplayName = "(UTC+07:00) Bangkok, Jakarta, Hanoi", Offset = "+07:00", TimeSpan = TimeSpan.FromHours(7) },
            new() { DisplayName = "(UTC+08:00) Shanghai, Singapore, Hong Kong", Offset = "+08:00", TimeSpan = TimeSpan.FromHours(8) },
            new() { DisplayName = "(UTC+08:30) Pyongyang", Offset = "+08:30", TimeSpan = TimeSpan.FromHours(8).Add(TimeSpan.FromMinutes(30)) },
            new() { DisplayName = "(UTC+09:00) Tokyo, Seoul", Offset = "+09:00", TimeSpan = TimeSpan.FromHours(9) },
            new() { DisplayName = "(UTC+09:30) Adelaide, Darwin", Offset = "+09:30", TimeSpan = TimeSpan.FromHours(9).Add(TimeSpan.FromMinutes(30)) },
            new() { DisplayName = "(UTC+10:00) Sydney, Melbourne", Offset = "+10:00", TimeSpan = TimeSpan.FromHours(10) },
            new() { DisplayName = "(UTC+10:30) Lord Howe Island", Offset = "+10:30", TimeSpan = TimeSpan.FromHours(10).Add(TimeSpan.FromMinutes(30)) },
            new() { DisplayName = "(UTC+11:00) Solomon Islands", Offset = "+11:00", TimeSpan = TimeSpan.FromHours(11) },
            new() { DisplayName = "(UTC+12:00) Fiji, New Zealand", Offset = "+12:00", TimeSpan = TimeSpan.FromHours(12) },
            new() { DisplayName = "(UTC+12:45) Chatham Islands", Offset = "+12:45", TimeSpan = TimeSpan.FromHours(12).Add(TimeSpan.FromMinutes(45)) },
            new() { DisplayName = "(UTC+13:00) Tonga, Samoa", Offset = "+13:00", TimeSpan = TimeSpan.FromHours(13) },
            new() { DisplayName = "(UTC+14:00) Kiribati, Line Islands", Offset = "+14:00", TimeSpan = TimeSpan.FromHours(14) }
        }.AsReadOnly();
    }
}
