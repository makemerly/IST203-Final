﻿Option Strict On
Option Explicit On
'TODO HANDLE CHECKBOXES
Public Class SoundsBadForm

    Private Sub WhatSoundsGoodForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'main decision progress
        If DecisionControl.Progress = 4 Then
            DecisionControl.RestMasterList = DBUtilities.GetRestList()
            DecisionControl.Progress = 5
        End If

        'reset/load lists
        LoadLists()

        clstRest.Sorted = True
        clstTags.Sorted = True
    End Sub

    'reset and re-load local lists from decisioncontrol lists
    Private Sub LoadLists()
        clstTags.Items.Clear()
        clstRest.Items.Clear()

        For Each item In DecisionControl.RestMasterList
            clstRest.Items.Add(item)
        Next
        For Each item In DecisionControl.TagMasterList
            clstTags.Items.Add(item)
        Next

        'if dc.restsoundsbad has restaurants, check them in this control
        If DecisionControl.RestSoundsBad.Count > 0 Then
            For Each item In clstRest.Items
                If DecisionControl.RestSoundsBad.Contains(CType(item, Restaurant)) Then
                    clstRest.SetItemChecked(clstRest.Items.IndexOf(item), True)
                End If

            Next
        End If
        'if dc.tagsoundsbad has tags, check them in this control
        If DecisionControl.TagsSoundsBad.Count > 0 Then
            For Each item In clstTags.Items
                If DecisionControl.TagsSoundsBad.Contains(CType(item, Tag)) Then
                    clstTags.SetItemChecked(clstTags.Items.IndexOf(item), True)
                End If
            Next
        End If

    End Sub

    'update decisioncontrol.restsoundsbad,tagssoundsbad
    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click

        DecisionControl.RestSoundsBad.Clear()
        For Each item In clstRest.CheckedItems
            DecisionControl.RestSoundsBad.Add(CType(item, Restaurant))
        Next

        DecisionControl.TagsSoundsBad.Clear()
        For Each item In clstTags.CheckedItems
            DecisionControl.TagsSoundsBad.Add(CType(item, Tag))
        Next

        If DecisionControl.Progress < 5 Then
            DecisionControl.Progress = 5
        End If

        Me.Close()
    End Sub

End Class