// Note this only includes basic configuration for development mode.
// For a more comprehensive configuration check:
// https://github.com/fable-compiler/webpack-config-template

var path = require("path");

module.exports = {
    mode: "development",
    entry: "./src/App.fs.js",
    output: {
        path: path.join(__dirname, "./public"),
        filename: "bundle.js",
    },
    devServer: {
        static: {
            directory: path.resolve(__dirname, "./public"),
            publicPath: "/",
        },
        port: 8090,
        proxy: {
            '/IStudentApi/*': { // tell webpack-dev-server to re-route all requests from client to the server
              target: "http://localhost:8080",// assuming the backend server is hosted on port 5000 during development
              changeOrigin: true
            }
        }
    },
    module: {
    }
}
