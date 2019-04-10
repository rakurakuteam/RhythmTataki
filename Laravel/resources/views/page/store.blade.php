@extends('master')

@section('title')
    스토어
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/userPage/userPage.css?s')}}" />
@endsection

@section('header')
    @include('layouts.header')
@endsection

@section('nav')

@endsection

@section('body')
    @include('components.store.index')
@endsection

@section('footer')

@endsection

@push('scripts')

@endpush