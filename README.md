# Sutil with Fable.Remoting

## Start the server:

```
(cd Server; dotnet run)
```

## Build Client

```
cd Client
npm install
```

## Start Client

```
npm run start
```

View the application at http://localhost:8090

Note how how `webpack.config.js` uses `proxy` to redirect API calls to the server at port 8080
