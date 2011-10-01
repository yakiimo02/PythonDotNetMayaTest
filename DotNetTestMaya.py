"""
Python.Net Maya Test

References:
        http://tech-artists.org/wiki/PythonNetInMaya

Tested on Maya 2012 64-bit (No HotFixes applied.)

Notes:
        Python.NET runs as a Modal dialog in its own GUI thread.
        Since the Maya command architecture is not thread-safe, I run Maya
        commands from the main Maya thread using executeInMainThreadWithResult.
        I experimented with running maya.cmds from the GUI thread and the cmds
        seem to run without problems except for maya.cmds.listRelatives, which
        would display a Python.Runtime.PythonException message inside a .NET
        message box and not run (no Maya crash, just a fail error message box.)

        Maya 2011 help section on "Python and threading"
        http://autodesk.com/us/maya/2011help/index.html?url=./files/Python_Python_and_threading.htm,topicNumber=d0e194482

Usage:
        1)
        Load DotNetTestMayaCmd script plugin and type

        import maya
        maya.cmds.DotNetTestMayaCmd()

        2)
        or just load the script in the script editor and call

        runDotNetTest()

written on 2011/09/30
by yakiimo02

"""

import clr
import sys
import threading
import maya.cmds
import maya.utils

import maya.OpenMaya as OpenMaya
import maya.OpenMayaMPx as OpenMayaMPx

# Change path to your actual PythonDotNetTest.dll
sys.path.append("C:\\PythonDotNetTest\\PythonDotNetTest\\bin\\x64\\Release")
clr.AddReference("PythonDotNetTest")
import PythonDotNetTest

def getTransform( shape ):
    """ return shape's parent transform. """
    parents = maya.cmds.listRelatives( shape, parent=True, fullPath=True )
    if parents:
        transform = parents[0]
    else:
        transform = ""
    return transform

class DotNetTest():
    def __init__(self):
        """ Create the Test WinForm and register event handlers. """
        self.wf = PythonDotNetTest.TestForm()
        self.wf.makeSphereBtn.Click += self.makeSphere
        self.wf.makeCubeBtn.Click += self.makeCube
        self.wf.updateGeomInfoBtn.Click += self.updateGeometryInfo
        self.wf.dataGridView.CellValueChanged += self.dataGridView_CellValueChanged
        self.wf.dataGridView.UserDeletedRow += self.dataGridView_UserDeletedRow
        self.wf.dataGridView.SelectionChanged += self.dataGridView_SelectionChanged

    def show(self):
        """ Display a Modal WinForm. """
        self.wf.ShowDialog()

    def makeSphere(*args):
        def sphereCmd():
            maya.cmds.polySphere()
        maya.utils.executeInMainThreadWithResult( sphereCmd )

    def makeCube(*args):
        def cubeCmd():
            maya.cmds.polyCube()
        maya.utils.executeInMainThreadWithResult( cubeCmd )

    def updateGeometryInfo(self,*args):
        """ Update the Test WinForm DataGridView with the Maya scene's geometryShape translation information. """
        self.wf.dataGridView.Rows.Clear()
        allGeoms = maya.utils.executeInMainThreadWithResult( maya.cmds.ls, type='geometryShape' )
        for geoms in allGeoms:
            transform = maya.utils.executeInMainThreadWithResult( getTransform, geoms )
            self.wf.AddRow( transform,
                    maya.utils.executeInMainThreadWithResult( maya.cmds.getAttr, transform + ".translateX" ),
                    maya.utils.executeInMainThreadWithResult( maya.cmds.getAttr, transform + ".translateY" ),
                    maya.utils.executeInMainThreadWithResult( maya.cmds.getAttr, transform + ".translateZ" ) )

    def dataGridView_CellValueChanged(self, sender, eventArgs):
        """ Update the the Maya scene's geometryShape translation information. """
        transform = self.wf.dataGridView.Rows[eventArgs.RowIndex].Cells[0].Value
        newVal = float( self.wf.dataGridView.Rows[eventArgs.RowIndex].Cells[eventArgs.ColumnIndex].Value )

        if eventArgs.ColumnIndex == 1:
            maya.utils.executeInMainThreadWithResult( maya.cmds.setAttr, transform+".translateX", newVal )
        elif eventArgs.ColumnIndex == 2:
            maya.utils.executeInMainThreadWithResult( maya.cmds.setAttr, transform+".translateY", newVal )
        elif eventArgs.ColumnIndex == 3:
            maya.utils.executeInMainThreadWithResult( maya.cmds.setAttr, transform+".translateZ", newVal )

    def dataGridView_UserDeletedRow(self, send, eventArgs):
        """ Delete geometryShape from the Maya scene. """
        transform = eventArgs.Row.Cells[0].Value
        maya.utils.executeInMainThreadWithResult( maya.cmds.delete, transform )

    def dataGridView_SelectionChanged(self, sender, eventArgs):
        """ Select the selected geometryShape in the Maya scene. """
        # Clear previous selection only if new rows have been selected.
        if self.wf.dataGridView.SelectedRows.Count > 0:
            maya.utils.executeInMainThreadWithResult( maya.cmds.select, clear=True )
        for row in self.wf.dataGridView.SelectedRows:
            name = row.Cells[0].Value
            maya.utils.executeInMainThreadWithResult( maya.cmds.select, name, add=True )

def runDotNetTest():
    """ Run the Test WinForm in a separate GUI Thread. """
    def runDotNetTestThreaded():
        print "GUIThread begin!"
        test = DotNetTest()
        test.show()
        print "GUIThread finished!"
    guiThread = threading.Thread( target = runDotNetTestThreaded, args = (), name="GUIThread" )
    guiThread.start()

kPluginCmdName = "DotNetTestMayaCmd"

# command
class scriptedCommand(OpenMayaMPx.MPxCommand):
    def __init__(self):
        OpenMayaMPx.MPxCommand.__init__(self)
    def doIt(self,argList):
        runDotNetTest()

# Creator
def cmdCreator():
    return OpenMayaMPx.asMPxPtr( scriptedCommand() )

# Initialize the script plug-in
def initializePlugin(mobject):
    mplugin = OpenMayaMPx.MFnPlugin(mobject,"yakiimo02", "1.0", "Any")
    try:
        mplugin.registerCommand( kPluginCmdName, cmdCreator )
    except:
        sys.stderr.write( "Failed to register command: %s\n" % kPluginCmdName )
        raise

# Uninitialize the script plug-in
def uninitializePlugin(mobject):
    mplugin = OpenMayaMPx.MFnPlugin(mobject)
    try:
        mplugin.deregisterCommand( kPluginCmdName )
    except:
        sys.stderr.write( "Failed to unregister command: %s\n" % kPluginCmdName )
        raise