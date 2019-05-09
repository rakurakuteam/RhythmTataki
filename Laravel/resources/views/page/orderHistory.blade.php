@extends('master')

@section('title')
    소리마당
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/store/orderHistory.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/footer.css')}}" />
@endsection

@section('header')
    @include('layouts.header.store')
@endsection

@section('body')
    @include('components.store.orderHistory')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
@endpush
