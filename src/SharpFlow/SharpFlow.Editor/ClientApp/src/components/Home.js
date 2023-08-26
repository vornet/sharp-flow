import React, { useCallback, useEffect, useState, useRef } from 'react';

import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Popover, { PopoverProps } from '@mui/material/Popover';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';

import axios from 'axios';
import ReactFlow, {
  MiniMap,
  Controls,
  Background,
  Panel,
  useNodesState,
  useEdgesState,
  addEdge,
  useReactFlow,
  ReactFlowProvider
} from 'reactflow';
import SharpflowNode from './SharpFlowNode';

import 'reactflow/dist/style.css';
import '../sharpflow.css';

let id = 1;
const getId = () => `${id++}`;

const nodeTypes = {
  sharpflow: SharpflowNode,
};

function FlowCanvas() {
  const [open, setOpen] = React.useState(false);
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [rfInstance, setRfInstance] = useState(null);
  const reactFlowWrapper = useRef(null);
  const connectingNode = useRef(null);
  const [nodes, setNodes, onNodesChange] = useNodesState();
  const [edges, setEdges, onEdgesChange] = useEdgesState();
  const { project } = useReactFlow();

  useEffect(() => {
    axios.get('/api/graph/1')
    .then(function (response) {
      setNodes(response.data.nodes);
      console.log(response.data.nodes);
      setEdges(response.data.edges);
    })
    .catch(function (error) {
      console.log(error);
    });
  }, []);

  const onConnect = useCallback((params) => setEdges((eds) => {
    console.log(eds);
    addEdge(params, eds)
  }), []);

  const onConnectStart = useCallback((_, { nodeId, handleId, handleType }) => {
    connectingNode.current = {nodeId, handleId}
  }, []);

  const handleClose = () => {
    setOpen(false);
  };

  const onConnectEnd = useCallback(
    (event) => {
      const targetIsPane = event.target.classList.contains('react-flow__pane');

      if (targetIsPane) {        
        setOpen(true);
        let getBoundingClientRect = () => new DOMRect(event.clientX, event.clientY);
        setAnchorEl({ getBoundingClientRect, nodeType: 1 });
      }
    },
    [project]
  );

  const onAddNode = () => {
    // we need to remove the wrapper bounds, in order to get the correct position
    const { top, left } = reactFlowWrapper.current.getBoundingClientRect();

    const id = getId();
    const newNode = {
      id,
      type: 'sharpflow',
      // we are removing the half of the node width (75) to center the new node
      position: project({ x: anchorEl.getBoundingClientRect().x - left - 75, y: anchorEl.getBoundingClientRect().y - top }),
      data: { label: `Node ${id}`, handles:[{id: 'foo', type: 'target'}] },
    };

    setNodes((nds) => nds.concat(newNode));
    setEdges((eds) => eds.concat({ id, source: connectingNode.current.nodeId, sourceHandle: connectingNode.current.handleId, target: id, targetHandle: 'foo' }));
    setOpen(false);
  };

  const onSave = useCallback(() => {
    if (rfInstance) {
      const flow = rfInstance.toObject();
      console.log(flow);
    }
  }, [rfInstance]);

  const onRun = useCallback(() => {
    if (rfInstance) {
      const flow = rfInstance.toObject();
      console.log(flow);
    }
  }, [rfInstance]);

  return (
      <Box style={{ width: 'calc(100vw - 20px)', height: 'calc(100vh - 80px)' }} ref={reactFlowWrapper}>
        <ReactFlow
          onInit={setRfInstance}
          nodes={nodes}
          edges={edges}
          onNodesChange={onNodesChange}
          onEdgesChange={onEdgesChange}
          onConnect={onConnect}
          onConnectStart={onConnectStart}
          onConnectEnd={onConnectEnd}
          nodeTypes={nodeTypes}
          fitView
        >
          <Panel position="top-right">
            <Button onClick={onRun}>Run</Button>
            <Button onClick={onSave}>Save</Button>
          </Panel>
          <Controls />
          <MiniMap />
          <Background color="#aaa" gap={16} />
        </ReactFlow>
        <Popover
          id={id}
          open={open}
          anchorEl={anchorEl}
          anchorOrigin={{ vertical: 'bottom', horizontal: 'left' }}
          onClose={handleClose}
        >
          <Paper>
            <Button onClick={onAddNode}>Add a node</Button>
          </Paper>
        </Popover>
      </Box>
  );
}

export function Home(props) {
  return (
    <ReactFlowProvider>
      <FlowCanvas {...props} />
    </ReactFlowProvider>
  )
}