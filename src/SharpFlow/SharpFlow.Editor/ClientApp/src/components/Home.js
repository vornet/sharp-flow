import React, { useCallback, useEffect, useState } from 'react';
import axios from 'axios';
import ReactFlow, {
  MiniMap,
  Controls,
  Background,
  Panel,
  useNodesState,
  useEdgesState,
  addEdge,
} from 'reactflow';
import CustomNode from '../CustomNode';

import 'reactflow/dist/style.css';
import '../custom.css';

const nodeTypes = {
  custom: CustomNode,
};

export function Home() {
  const [rfInstance, setRfInstance] = useState(null);
  const [nodes, setNodes, onNodesChange] = useNodesState();
  const [edges, setEdges, onEdgesChange] = useEdgesState();

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

  const onSave = useCallback(() => {
    if (rfInstance) {
      const flow = rfInstance.toObject();
      console.log(flow);
    }
  }, [rfInstance]);

  return (
    <div style={{ width: '100vw', height: '100vh' }}>
      <ReactFlow
        onInit={setRfInstance}
        nodes={nodes}
        edges={edges}
        onNodesChange={onNodesChange}
        onEdgesChange={onEdgesChange}
        onConnect={onConnect}
        nodeTypes={nodeTypes}
        fitView
      >
        <Panel position="top-right">
          <button onClick={onSave}>save</button>
        </Panel>
        <Controls />
        <MiniMap />
        <Background color="#aaa" gap={16} />
      </ReactFlow>
    </div>
  );
}