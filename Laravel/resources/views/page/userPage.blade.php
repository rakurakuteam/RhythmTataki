@extends('master')

@section('title')
    사용자 페이지
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/userPage/userPage.css?s')}}" />
@endsection

@section('header')
    @include('layouts.header.main')
@endsection

@section('nav')

@endsection

@section('body')
    @include('components.userPage.content')
@endsection

@section('footer')

@endsection

@push('scripts')

@endpush