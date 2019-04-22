@extends('master')

@section('title')
    게시물
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/board/board.css?s')}}" />
@endsection

@section('header')
    @include('layouts.header.main')
@endsection

@section('nav')

@endsection

@section('body')
    @include('components.board.content')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
    <script src="{{asset('js/heart.js')}}"></script>
@endpush