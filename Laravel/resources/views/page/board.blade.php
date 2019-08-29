@extends('master')

@section('title')
    게시물
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/board/post.css')}}" />
    {{-- <link rel="stylesheet" type="text/css" href="{{asset('css/board/video.css')}}" /> --}}
    {{-- <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"> 부트스트랩 CSS --}}
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"> {{-- 부트스트랩 --}}
@endsection

@section('head')
    {{header("Access-Control-Allow-Origin: *")}}
@endsection

@section('header')
    @include('layouts.header.main')
@endsection

@section('body')
    @include('components.board.content')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
    <script src="{{asset('js/board/heart.js')}}"></script>
    <script src="{{asset('js/board/package.js')}}"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script> {{-- 부트스트랩 --}}
@endpush
