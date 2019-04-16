@extends('master')

@section('title')
    게시물
@endsection

@section('style')
<link rel="stylesheet" type="text/css" href="{{asset('css/board/board.css')}}" />
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

@endsection

@push('scripts')
    <script src="{{asset('js/heart.js')}}"></script>
@endpush