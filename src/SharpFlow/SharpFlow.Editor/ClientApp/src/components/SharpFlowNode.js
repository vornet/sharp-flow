import React, { memo } from 'react';
import { Handle, Position, useReactFlow, useStoreApi } from 'reactflow';

function Select({ handleId, nodeId, position, type }) {
    const { setNodes } = useReactFlow();
    const store = useStoreApi();
  
    const onChange = (evt) => {
      const { nodeInternals } = store.getState();
      setNodes(
        Array.from(nodeInternals.values()).map((node) => {
          if (node.id === nodeId) {
            node.data = {
              ...node.data,
              selects: {
                ...node.data.selects,
                [handleId]: evt.target.value,
              },
            };
          }
  
          return node;
        })
      );
    };
  
    return (
      <div className="custom-node__select">
        <div>{handleId}</div>
        <Handle type={type} position={position} id={handleId} />
      </div>
    );
}

function SharpFlowNode({ id, data }) {
  return (
    <>
        <div className="custom-node__header">
            <strong>{data.label}</strong>
        </div>
        <div className="custom-node__body">
            <div className="custom-node__column-left">
                {data.handles.filter(handle => handle.type === "target").map((handle) => (                    
                    <Select key={handle.id} nodeId={id} handleId={handle.id} position={Position.Left} type={handle.type} />
                ))}
            </div>
            <div className="custom-node__column-right">
            {data.handles.filter(handle => handle.type === "source").map((handle) => (
                    <Select key={handle.id} nodeId={id} handleId={handle.id} position={Position.Right} type={handle.type}/>
                ))}
            </div>
        </div>
    </>
  );
}

export default memo(SharpFlowNode);