namespace CarryLoad.Application
{
    public static class Hubs
    {
        private const string Root = "hub";
        private const string Version = "v1";

        private const string Base = Root + "/" + Version;
        private const string ReceiveMessage = "receiveMessage";

        public static class DriverHub
        {
            public const string Name = Base + "/driver";

            /// <summary>
            /// Subscribe to receive the order requests
            /// </summary>
            public const string OrderRequestMessage = ReceiveMessage + "/orderRequest";

            /// <summary>
            /// Subscribe to receive the results of the order request
            /// </summary>
            public const string ResultOrderMessage = ReceiveMessage + "/resultOrder";
        }
    }
}
