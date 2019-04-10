<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <meta name="csrf-token" content="{{ csrf_token() }}">

    <title>@yield('title', "메인페이지")</title>
    <script src="http://code.jquery.com/jquery-latest.js"></script>

    {{-- <link rel="stylesheet" href="{{ mix('css/app.css') }}"> --}}
    @yield('style')
    @stack('scripts')
</head>
<body>
    @yield('header')
    @yield('nav')
    @yield('body')
    @yield('footer')
    <script src="{{ mix('js/app.js') }}"></script>
</body>
</html>