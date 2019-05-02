@extends('master')

@section('title')
    로그인
@endsection

@section('meta')
    <meta
    name="google-signin-client_id"
    content="533599203756-u4dfure95b78rd0o9och7qc532t094k1.apps.googleusercontent.com">
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
@endsection

@section('header')
    @include('layouts.header.main')
@endsection

@section('nav')
@endsection

@section('body')
    @include('components.auth.login')
    @include('components.auth.googleAuth')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
    <script
    src="https://apis.google.com/js/platform.js"
    async="async"
    defer="defer"></script>
@endpush