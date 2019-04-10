@extends('master')

@section('title')
    소리마당
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/main/main_2.css?s')}}" />
@endsection

@section('header')
    @include('layouts.header')
@endsection

@section('nav')
    @include('components.main.slide')
@endsection

@section('body')
    @include('components.main.content')
@endsection

@section('footer')

@endsection

@push('scripts')
    <script src="{{asset('js/heart.js')}}"></script>
    <script src="{{asset('js/pagination.js')}}"></script>
@endpush