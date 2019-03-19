<!DOCTYPE html>
<html lang="en">
    <head>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="ie=edge">
        <meta
            name="google-signin-client_id"
            content="533599203756-u4dfure95b78rd0o9och7qc532t094k1.apps.googleusercontent.com">
        <title>Document</title>
        <script
            src="https://apis.google.com/js/platform.js"
            async="async"
            defer="defer"></script>
    </head>
    <body>
        <div class="g-signin2" data-onsuccess="onSignIn" onclick="location.href='/login/google'"></div>
        <script>
            function onSignIn(googleUser) {
                var profile = googleUser.getBasicProfile();
                console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
                console.log('Name: ' + profile.getName());
                console.log('Image URL: ' + profile.getImageUrl());
                console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.
            }
        </script>
    </body>
</html>