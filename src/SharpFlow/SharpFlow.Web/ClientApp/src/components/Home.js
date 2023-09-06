import React, { useCallback, useEffect, useState, useRef } from 'react';

import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Popover from '@mui/material/Popover';
import Paper from '@mui/material/Paper';
import MenuList from '@mui/material/MenuList';
import MenuItem from '@mui/material/MenuItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemIcon from '@mui/material/ListItemIcon';
import Cloud from '@mui/icons-material/Cloud';

import axios from 'axios';
import ReactFlow, {
  MiniMap,
  Controls,
  Background,
  Panel,
  applyNodeChanges,
  applyEdgeChanges,
  addEdge,
  useReactFlow,
  ReactFlowProvider
} from 'reactflow';
import SharpflowNode from './SharpFlowNode';
import SecretsStringNode from './SecretsStringNode';
import LiteralStringNode from './LiteralStringNode';
import LiteralIntNode from './LiteralIntNode';
import LiteralDoubleNode from './LiteralDoubleNode';

import 'reactflow/dist/style.css';
import '../sharpflow.css';
import { Typography } from '@mui/material';

let id = 1;
const getId = () => `${id++}`;

const nodeTypes = {
  sharpflow: SharpflowNode,
  secretsString: SecretsStringNode,
  literalString: LiteralStringNode,
  literalInt: LiteralIntNode,
  literalDouble: LiteralDoubleNode,
};

function FlowCanvas() {
  const [nodePickerOpen, setNodePickerOpen] = React.useState(false);
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [rfInstance, setRfInstance] = useState(null);
  const reactFlowWrapper = useRef(null);
  const connectingNode = useRef(null);
  const [metadata, setMetadata] = useState(null);
  const [loggerOutput, setLoggerOutput] = useState("");
  const [nodes, setNodes] = useState([]);
  const [edges, setEdges] = useState([]);
  const { project } = useReactFlow();

  useEffect(() => {
    axios.get('/api/metadata')
    .then(function (response) {
      setMetadata(response.data);
    })
    .catch(function (error) {
      console.log(error);
    });

    axios.get('/api/graph/foo')
    .then(function (response) {

      response.data.nodes.forEach(node => {
        node.data.changeNodeId = onChangeNodeId;
      });

      let secretsNodes = response.data.nodes.filter(n => n.type==='secretsString');

      secretsNodes.forEach(node => {
        node.data.update = onNodeChange;
      });

      setNodes(response.data.nodes);
      setEdges(response.data.edges);
    })
    .catch(function (error) {
      console.log(error);
    });
  }, []);

  const onConnect = useCallback(
    (params) => setEdges((eds) => addEdge(params, eds)),
    [setEdges]
  );

  const onNodesChange = useCallback(
    (changes) => setNodes((nds) => applyNodeChanges(changes, nds)),
    [setNodes]
  );
  const onEdgesChange = useCallback(
    (changes) => setEdges((eds) => applyEdgeChanges(changes, eds)),
    [setEdges]
  );

  const onConnectStart = useCallback((_, { nodeId, handleId, handleType }) => {
    console.log("connection started.");
    const node = nodes.find(node => node.id === nodeId);
    const handle = node?.data.handles.find(handle => handle.id === handleId);
    connectingNode.current = {node, handle}    
  }, [nodes]);

  const handleClose = () => {
    setNodePickerOpen(false);
  };

  const onConnectEnd = useCallback(
    (event) => {
      const targetIsPane = event.target.classList.contains('react-flow__pane');

      if (targetIsPane) {        
        setNodePickerOpen(true);
        let getBoundingClientRect = () => new DOMRect(event.clientX, event.clientY);
        setAnchorEl({ getBoundingClientRect, nodeType: 1 });
      }
    },
    [project]
  );

  const onNodeChange = (id, value) => {
    setNodes((nds) =>
      nds.map((node) => {
        if (node.id !== id) {
          return node;
        }

        let newState = {}

        if (node.type === "secretsString") {
          newState = { Secret: value }
        } else if (node.type === "literalString") {
          newState = { Text: value }
        } else if (node.type === "literalInt") {
          newState = { Number: value }
        } else if (node.type === "literalDouble") {
          newState = { Number: value }
        }

        return {
          ...node,
          data: {
            ...node.data,
            state: newState,
          },
        };
      })
    );
  };

  const onChangeNodeId = (oldId, newId) => {
    setNodes((nds) =>
      nds.map((node) => {
        if (node.id !== oldId) {
          return node;
        }

        return {
          ...node,
          id: newId,
        };
      })
    );
  };


  const onAddNode = useCallback((node) => {
    // we need to remove the wrapper bounds, in order to get the correct position
    const { top, left } = reactFlowWrapper.current.getBoundingClientRect();

    let id = `NewNode${getId()}`;

    while (nodes.find(node => node.id === id))
    {
      id = `NewNode${getId()}`;
    }

    let type = 'sharpflow';

    if (node.type === "VorNet.SharpFlow.Engine.Nodes.SecretStringNode") {
      type = 'secretsString'
    } else if (node.type === "VorNet.SharpFlow.Engine.Nodes.LiteralStringNode") {
      type = 'literalString'
    } else if (node.type === "VorNet.SharpFlow.Engine.Nodes.LiteralIntNode") {
      type = 'literalInt'
    } else if (node.type === "VorNet.SharpFlow.Engine.Nodes.LiteralDoubleNode") {
      type = 'literalDouble'
    }

    console.log(node.handles);

    const newNode = {
      id: id,
      type: type,
      // we are removing the half of the node width (75) to center the new node
      position: project({ x: anchorEl.getBoundingClientRect().x - left - 75, y: anchorEl.getBoundingClientRect().y - top }),
      data: { type: node.type, displayType: node.displayType, handles: node.handles, update: onNodeChange, changeNodeId: onChangeNodeId },
    };

    setNodes((nds) => nds.concat(newNode));

    let matchingHandle = null;
    
    if (connectingNode.current) {
      matchingHandle = node.handles.find(handle => handle.type === connectingNode.current.handle.type);
    }
    if (connectingNode.current && matchingHandle) {
      setEdges((eds) => eds.concat({ id, source: connectingNode.current.node.id, sourceHandle: connectingNode.current.handle.id, target: id, targetHandle: matchingHandle.id }));
    }
    
    connectingNode.current = null;
    setNodePickerOpen(false);
  }, [anchorEl, connectingNode]);

  const isValidConnection = (connection) => {
    const sourceHandle = nodes.find(node => node.id === connection.source).data.handles.find(handle => handle.id === connection.sourceHandle);
    const targetHandle = nodes.find(node => node.id === connection.target).data.handles.find(handle => handle.id === connection.targetHandle);
   
    return sourceHandle.type === targetHandle.type;
  }

  const onSave = useCallback(() => {
    if (rfInstance) {
      const flow = rfInstance.toObject();
      flow.name = "foo";
      axios.post('/api/graph', flow)
      .then(function (response) {
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
      });
    }
  }, [rfInstance]);

  const onRun = () => {
    axios.post('/api/executor/execute?name=foo')
    .then(function (response) {
      setLoggerOutput(response.data);
    })
    .catch(function (error) {
      console.log(error);
    });
  };

  const handleContextMenu = (event) => {
    event.preventDefault();

    setNodePickerOpen(true);
    let getBoundingClientRect = () => new DOMRect(event.clientX, event.clientY);
    setAnchorEl({ getBoundingClientRect, nodeType: 1 });

  };

  return (
      <Box onContextMenu={handleContextMenu} style={{ cursor: 'context-menu', width: 'calc(100vw - 20px)', height: 'calc(100vh - 80px)' }} ref={reactFlowWrapper}>
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
          isValidConnection={isValidConnection}
          fitView
        >
          <Panel position="top-right">
            <Paper>
              <Button onClick={onRun}>Run</Button>
              <Button onClick={onSave}>Save</Button>
            </Paper>
          </Panel>
          <Panel position="bottom-center">
            <Paper sx={{ width: 500, height: 200, padding: 1, whiteSpace: 'pre-wrap' }}>
              <Typography>
                {loggerOutput}
              </Typography>
            </Paper>
          </Panel>
          <Controls />
          <MiniMap />
          <Background color="#aaa" gap={16} />
        </ReactFlow>
        <Popover
          id={id}
          open={nodePickerOpen}
          anchorEl={anchorEl}
          anchorOrigin={{ vertical: 'bottom', horizontal: 'left' }}
          onClose={handleClose}
        >
          <Paper sx={{ width: 320, maxWidth: '100%' }}>
            <MenuList>
              {metadata?.nodes?.map(node => (
                <MenuItem key={node.displayType} onClick={() => onAddNode(node)}>
                <ListItemIcon>
                  <Cloud fontSize="small" />
                </ListItemIcon>
                <ListItemText>{node.displayType}</ListItemText>
              </MenuItem>))}
            </MenuList>
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