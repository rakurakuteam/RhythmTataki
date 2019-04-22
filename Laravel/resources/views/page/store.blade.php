@extends('master')

@section('title')
    스토어
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/store/storeMain.css?ss')}}" />
@endsection

@section('header')
    @include('layouts.header.store')
@endsection

@section('nav')
    @include('components.main.slide')
@endsection

@section('body')
    @include('components.store.index')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
@endpush