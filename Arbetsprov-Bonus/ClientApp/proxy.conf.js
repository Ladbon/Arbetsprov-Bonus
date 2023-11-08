const { env } = require('process');

const PROXY_CONFIG = [
  {
    context: ["/consultant"],
    target: "http://localhost:5076", // Adjusted to match the .NET server port
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
];

module.exports = PROXY_CONFIG;
