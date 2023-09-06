import React, { memo } from 'react';
import { Handle, Position, useReactFlow, useStoreApi } from 'reactflow';

function Select({ handle, position }) { 
    return (
      <div className="custom-node__select">
        <div>{handle.id} : {handle.displayType} </div>
        <Handle type={handle.direction} position={position} id={handle.id} />
      </div>
    );
}

function SharpFlowNode({ id, data }) {
  
  const onIdBlur = React.useCallback(evt => {
    data.changeNodeId(id, evt.currentTarget.innerHTML);
  }, [])

  return (
    <>
        <div className="custom-node__header">
            <strong><span contentEditable onBlur={onIdBlur} dangerouslySetInnerHTML={{__html: id}} />{data.displayType?.length > 0 && ` : ${data.displayType}`}</strong>
        </div>
        <div className="custom-node__body">
            <div className="custom-node__column-left">
                {data.handles.filter(handle => handle.direction === "target").map((handle) => (                    
                    <Select key={handle.id} nodeId={id} handle={handle} position={Position.Left} />
                ))}
            </div>
            <div className="custom-node__column-right">
            {data.handles.filter(handle => handle.direction === "source").map((handle) => (
                    <Select key={handle.id} nodeId={id} handle={handle} position={Position.Right} />
                ))}
            </div>
        </div>
    </>
  );
}

export default memo(SharpFlowNode);