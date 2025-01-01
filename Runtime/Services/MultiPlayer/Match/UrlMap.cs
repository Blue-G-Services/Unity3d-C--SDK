namespace DynamicPixels.GameService.Services.MultiPlayer.Match
{
    public class UrlMap
    {
        private const string _baseUrl = "/api/services/multiplayer/matches";

        // post
        public static string MakeMatchUrl = $"{_baseUrl}";
        public static string MakeAndStartMatchUrl = $"{_baseUrl}/makeAndStart";

        // get
        public static string GetMyMatchesUrl = $"{_baseUrl}";
        public static string LoadMatch(int matchId) => $"{_baseUrl}/{matchId}";
        public static string LoadMatchByRoomId(int roomId) => $"{_baseUrl}/room/{roomId}";
        public static string LoadState(int matchId, string stateKey) => $"{_baseUrl}/{matchId}/states/{stateKey}";

        //put
        public static string SaveUrl(int matchId) => $"{_baseUrl}/{matchId}";
        public static string SaveState(int matchId, string stateKey) => $"{_baseUrl}/{matchId}/states/{stateKey}";
        public static string SavePlayerMetaDataUrl(int matchId) => $"{_baseUrl}/{matchId}/players";
        public static string FinishMatchUrl(int matchId) => $"{_baseUrl}/{matchId}/finish";

        // patch
        public static string UpdateMatchStatusUrl(int matchId) => $"{_baseUrl}/{matchId}";

        // delete
        public static string LeaveAndAbortUrl(int matchId) => $"{_baseUrl}/{matchId}/players";

    }
}