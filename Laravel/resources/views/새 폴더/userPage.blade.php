@extends('master')

@section('title')
    마이페이지
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/userPage/userPage.css')}}" />
@endsection

@section('header')
    @include('layouts.header')
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