@extends('master')

@section('title')
    소리마당
@endsection

@section('style')
    <link rel="stylesheet" type="text/css" href="{{asset('css/header.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/store/historyDetail.css')}}" />
    <link rel="stylesheet" type="text/css" href="{{asset('css/footer.css')}}" />
@endsection

@section('header')
    @include('layouts.header.main')
@endsection

@section('body')
    @include('components.store.historyDetail')
@endsection

@section('footer')
    @include('layouts.footer.footer')
@endsection

@push('scripts')
@endpush
