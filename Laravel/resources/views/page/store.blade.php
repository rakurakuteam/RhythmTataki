@extends('master')

@section('title')
    스토어
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/store/storeMain.css')}}" />
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

@endsection

@push('scripts')
@endpush