@extends('master')

@section('title')
    소리마당
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    {{-- <link rel="stylesheet" type="text/css" href="{{asset('css/main/main_2.css')}}" /> --}}
    {{-- <link rel="stylesheet" type="text/css" href="{{asset('css/footer.css')}}" /> --}}
    <link rel="stylesheet" type="text/css" href="{{asset('css/main/main.css')}}" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"> {{-- 부트스트랩 --}}
@endsection

@section('header')
    @include('layouts.header.main')
    @include('components.auth.loginModal')
@endsection

@section('nav')
    @include('components.main.slide')
@endsection

@section('body')
    @include('components.main.content')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
    <script src="{{asset('js/board/heart.js')}}"></script>
    <script src="{{asset('js/pagination.js')}}"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script> {{-- 부트스트랩 --}}
@endpush
