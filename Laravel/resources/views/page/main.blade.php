@extends('master')

@section('title')
    소리마당
@endsection

@section('style')
    {{-- <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" /> --}}
    {{-- <link rel="stylesheet" type="text/css" href="{{asset('css/main/main_2.css')}}" /> --}}
    {{-- <link rel="stylesheet" type="text/css" href="{{asset('css/footer.css')}}" /> --}}
    <link rel="stylesheet" type="text/css" href="{{asset('css/main/main.css')}}" />
@endsection

@section('header')
    @include('layouts.header.main')
@endsection

@section('nav')
    @include('components.main.slide')
@endsection

@section('body')
    @include('components.main.content')
@endsection

@section('footer')
    {{-- @include('layouts.footer.footer') --}}
@endsection

@push('scripts')
    <script src="{{asset('js/board/heart.js')}}"></script>
    <script src="{{asset('js/pagination.js')}}"></script>
@endpush
