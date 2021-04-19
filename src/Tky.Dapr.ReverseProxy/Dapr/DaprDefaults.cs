// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
// ------------------------------------------------------------

using System;

namespace Dapr
{
    internal static class DaprDefaults
    {
        private static string _httpEndpoint;
        private static string _grpcEndpoint;
        private static string _apiToken;

        public static string GetDefaultApiToken()
        {
            // Lazy-init is safe because this is just populating the default
            // We don't plan to support the case where the user changes environment variables
            // for a running process.
            if (_apiToken == null)
            {
                var value = Environment.GetEnvironmentVariable("DAPR_API_TOKEN");

                // Treat empty the same as null since it's an environment variable
                _apiToken = value == string.Empty ? null : _apiToken;
            }

            return _apiToken;

            
        }

        public static string GetDefaultHttpEndpoint()
        {
            if (_httpEndpoint == null)
            {
                var port = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");
                port = string.IsNullOrEmpty(port) ? "3500" : port;
                _httpEndpoint = $"http://127.0.0.1:{port}";
            }

            return _httpEndpoint;
        }

        public static string GetDefaultGrpcEndpoint()
        {
            if (_grpcEndpoint == null)
            {
                var port = Environment.GetEnvironmentVariable("DAPR_GRPC_PORT");
                port = string.IsNullOrEmpty(port) ? "50001" : port;
                _grpcEndpoint = $"http://127.0.0.1:{port}";
            }

            return _grpcEndpoint;
        }
    }
}
